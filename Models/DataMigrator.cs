using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DrAshrafMellouli.Models
{
    public static class DataMigrator
    {
        public static void MigrateSqliteToPostgres(AppDbContext targetContext, string sqliteDbPath)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            if (!File.Exists(sqliteDbPath))
            {
                Console.WriteLine($"[DataMigrator] SQLite database not found at '{sqliteDbPath}'. Skipping migration.");
                return;
            }

            Console.WriteLine($"[DataMigrator] Found SQLite database at '{sqliteDbPath}'. Inspecting data...");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite($"Data Source={sqliteDbPath}");

            using var sourceContext = new AppDbContext(optionsBuilder.Options);

            try
            {
                // 1. Migrate Treatments
                if (!targetContext.Treatments.Any())
                {
                    var treatments = sourceContext.Treatments.AsNoTracking().ToList();
                    if (treatments.Any())
                    {
                        Console.WriteLine($"[DataMigrator] Migrating {treatments.Count} Treatments...");
                        targetContext.Treatments.AddRange(treatments);
                        targetContext.SaveChanges();
                        ResetSequence(targetContext, "Treatments", "Id");
                    }
                }

                // 2. Migrate Results
                if (!targetContext.Results.Any())
                {
                    var results = sourceContext.Results.AsNoTracking().ToList();
                    if (results.Any())
                    {
                        Console.WriteLine($"[DataMigrator] Migrating {results.Count} Results...");
                        targetContext.Results.AddRange(results);
                        targetContext.SaveChanges();
                        ResetSequence(targetContext, "Results", "Id");
                    }
                }

                // 3. Migrate Articles
                if (!targetContext.Articles.Any())
                {
                    var articles = sourceContext.Articles.AsNoTracking().ToList();
                    if (articles.Any())
                    {
                        foreach (var article in articles)
                        {
                            article.DatePublished = DateTime.SpecifyKind(article.DatePublished, DateTimeKind.Utc);
                        }
                        Console.WriteLine($"[DataMigrator] Migrating {articles.Count} Articles...");
                        targetContext.Articles.AddRange(articles);
                        targetContext.SaveChanges();
                        ResetSequence(targetContext, "Articles", "Id");
                    }
                }

                // 4. Migrate Testimonials
                if (!targetContext.Testimonials.Any())
                {
                    var testimonials = sourceContext.Testimonials.AsNoTracking().ToList();
                    if (testimonials.Any())
                    {
                        Console.WriteLine($"[DataMigrator] Migrating {testimonials.Count} Testimonials...");
                        targetContext.Testimonials.AddRange(testimonials);
                        targetContext.SaveChanges();
                        ResetSequence(targetContext, "Testimonials", "Id");
                    }
                }

                // 5. Migrate Appointments
                if (!targetContext.Appointments.Any())
                {
                    var appointments = sourceContext.Appointments.AsNoTracking().ToList();
                    if (appointments.Any())
                    {
                        foreach (var appt in appointments)
                        {
                            appt.CreatedAt = DateTime.SpecifyKind(appt.CreatedAt, DateTimeKind.Utc);
                        }
                        Console.WriteLine($"[DataMigrator] Migrating {appointments.Count} Appointments...");
                        targetContext.Appointments.AddRange(appointments);
                        targetContext.SaveChanges();
                        ResetSequence(targetContext, "Appointments", "Id");
                    }
                }

                Console.WriteLine("[DataMigrator] SQLite to PostgreSQL data migration completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DataMigrator] Error during migration: {ex.Message}");
            }
        }

        private static void ResetSequence(AppDbContext context, string tableName, string columnName)
        {
            try
            {
                string sql = $@"
                    SELECT setval(
                        pg_get_serial_sequence('""{tableName}""', '{columnName}'),
                        COALESCE((SELECT MAX(""{columnName}"") FROM ""{tableName}""), 1)
                    );";
                context.Database.ExecuteSqlRaw(sql);
            }
            catch
            {
                // Ignore sequence reset error if table is not using identity sequence
            }
        }
    }
}
