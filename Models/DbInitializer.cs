using System;
using System.Collections.Generic;
using System.Linq;

namespace DrAshrafMellouli.Models
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            // Prepare the list of initial treatments
            var initialTreatments = new List<Treatment>
            {
                // 1. Visage (Face)
                new Treatment
                {
                    Title = "Botox (Toxine Botulique)",
                    Description = "Pour le traitement des rides d'expression (front, ride du lion, pattes d'oie).",
                    Category = Category.Face,
                    ImageUrl = "/images/medical_face.png"
                },
                new Treatment
                {
                    Title = "Comblement Ac. (Acide Hyaluronique)",
                    Description = "Pour restaurer les volumes du visage (cernes, pommettes, lèvres, sillons nasogéniens).",
                    Category = Category.Face,
                    ImageUrl = "/images/medical_face.png"
                },
                new Treatment
                {
                    Title = "MesoTherapie (Mésolift)",
                    Description = "Injections superficielles de vitamines et d'acide hyaluronique non réticulé pour réhydrater et donner de l'éclat à la peau.",
                    Category = Category.Face,
                    ImageUrl = "/images/medical_face.png"
                },
                new Treatment
                {
                    Title = "Sculptra / Radiesse",
                    Description = "Inducteurs de collagène et biostimulateurs injectables pour raffermir la peau et restaurer les volumes naturellement.",
                    Category = Category.Face,
                    ImageUrl = "/images/medical_face.png"
                },
                new Treatment
                {
                    Title = "Fils tenseurs",
                    Description = "Pose de fils de suspension sous la peau pour un effet lifting non chirurgical et la stimulation du collagène.",
                    Category = Category.Face,
                    ImageUrl = "/images/medical_face.png"
                },
                new Treatment
                {
                    Title = "Morpheus 8 (Visage)",
                    Description = "Technologie de radiofréquence fractionnée à micro-aiguilles pour retendre la peau, traiter les rides et les cicatrices.",
                    Category = Category.Face,
                    ImageUrl = "/images/medical_face.png"
                },
                new Treatment
                {
                    Title = "CFU Elife ⊕ (Visage)",
                    Description = "Traitement par ultrasons focalisés de haute intensité (HIFU) pour lifter le visage, redessiner l'ovale et raffermir la peau.",
                    Category = Category.Face,
                    ImageUrl = "/images/medical_face.png"
                },

                // 2. Hair (Cheveux)
                new Treatment
                {
                    Title = "Consultation Capillaire",
                    Description = "Bilan capillaire et diagnostic personnalisé de la chute des cheveux.",
                    Category = Category.Hair,
                    ImageUrl = "/images/medical_hair.png"
                },
                new Treatment
                {
                    Title = "PRP (Plasma Riche en Plaquettes)",
                    Description = "Injection de plasma autologue concentré en facteurs de croissance pour stimuler la repousse et densifier les cheveux.",
                    Category = Category.Hair,
                    ImageUrl = "/images/medical_hair.png"
                },
                new Treatment
                {
                    Title = "Mésothérapie par Exosomes Hair",
                    Description = "Soin régénérant de pointe utilisant des exosomes pour stimuler les follicules pileux fatigués.",
                    Category = Category.Hair,
                    ImageUrl = "/images/medical_hair.png"
                },
                new Treatment
                {
                    Title = "Regenera (Greffe de cellules souches)",
                    Description = "Traitement innovant (Regenera Activa) par micro-greffe autologue de cellules souches pour lutter contre l'alopécie androgénétique.",
                    Category = Category.Hair,
                    ImageUrl = "/images/medical_hair.png"
                },

                // 3. Laser
                new Treatment
                {
                    Title = "Laser CO2 : Resurfacing",
                    Description = "Pour lisser les rides profondes, améliorer le grain de peau et traiter le photovieillissement (relissage cutané).",
                    Category = Category.Laser,
                    ImageUrl = "/images/medical_laser.png"
                },
                new Treatment
                {
                    Title = "Laser CO2 : Traitement de cicatrice",
                    Description = "Réduction des cicatrices d'acné, chirurgicales ou traumatiques.",
                    Category = Category.Laser,
                    ImageUrl = "/images/medical_laser.png"
                },
                new Treatment
                {
                    Title = "Laser CO2 : Traitement des papillomes & verrues",
                    Description = "Élimination précise et sans saignement des excroissances cutanées (acrochordons, papillomes, verrues).",
                    Category = Category.Laser,
                    ImageUrl = "/images/medical_laser.png"
                },
                new Treatment
                {
                    Title = "Laser Vaginal : Rajeunissement vaginal",
                    Description = "Traitement de l'atrophie vaginale, de la sécheresse intime et de l'incontinence urinaire d'effort légère.",
                    Category = Category.Laser,
                    ImageUrl = "/images/medical_laser.png"
                },
                new Treatment
                {
                    Title = "Laser Épilation : Alexandrite / Nd:YAG (Cynosure Elite Pro)",
                    Description = "Appareil de référence double longueur d'onde pour traiter efficacement tous les phototypes de peau (peaux claires à très foncées).",
                    Category = Category.Laser,
                    ImageUrl = "/images/medical_laser.png"
                },
                new Treatment
                {
                    Title = "Laser Épilation : Laser Diode",
                    Description = "Alternative d'épilation laser performante.",
                    Category = Category.Laser,
                    ImageUrl = "/images/medical_laser.png"
                },
                new Treatment
                {
                    Title = "Photothérapie LED : Dôme LED (ou LED Lune)",
                    Description = "Traitement par photomodulation LED pour accélérer la cicatrisation, calmer l'inflammation après les soins et stimuler l'éclat de la peau.",
                    Category = Category.Laser,
                    ImageUrl = "/images/medical_laser.png"
                },

                // 4. Body (Corps)
                new Treatment
                {
                    Title = "Morpheus 8 (Body)",
                    Description = "Application du Morpheus8 sur le corps pour retendre la peau relâchée (ventre, bras, cuisses) et améliorer l'aspect de la cellulite.",
                    Category = Category.Body,
                    ImageUrl = "/images/medical_body.png"
                },
                new Treatment
                {
                    Title = "CFU Elife ⊖ (Body)",
                    Description = "Utilisation des ultrasons focalisés (HIFU) pour détruire les graisses localisées et raffermir les tissus corporels.",
                    Category = Category.Body,
                    ImageUrl = "/images/medical_body.png"
                }
            };

            bool changesSaved = false;

            foreach (var treatment in initialTreatments)
            {
                // Check if treatment with the same Title and Category already exists
                var exists = context.Treatments.Any(t => t.Title == treatment.Title && t.Category == treatment.Category);
                if (!exists)
                {
                    context.Treatments.Add(treatment);
                    changesSaved = true;
                }
            }

            // Prepare initial results
            var initialResults = new List<Result>
            {
                new Result
                {
                    CaseNumber = "#0821",
                    Title = "Sculpture Volumétrique & Rajeunissement Facial",
                    Description = "Restauration des volumes de la pommette, lissage des rides du contour des yeux et éclat cutané instantané.",
                    Category = Category.Face,
                    BeforeImageUrl = "/images/before_face_1.png",
                    AfterImageUrl = "/images/after_face_1.png",
                    ProtocolName = "ARCHITECTURE HYALURONIQUE & BOTOX",
                    Sessions = "1 séance",
                    ResultType = "Immédiat"
                },
                new Result
                {
                    CaseNumber = "#1042",
                    Title = "Resurfacing Cutané & Élimination des Taches",
                    Description = "Lissage des micro-reliefs cutanés, réduction des pores dilatés et unification parfaite du teint.",
                    Category = Category.Laser,
                    BeforeImageUrl = "/images/before_laser_2.png",
                    AfterImageUrl = "/images/after_laser_2.png",
                    ProtocolName = "LASER CO2 FRACTIONNÉ",
                    Sessions = "3 séances",
                    ResultType = "Durable"
                },
                new Result
                {
                    CaseNumber = "#0915",
                    Title = "Redensification Capillaire & Alopécie",
                    Description = "Stimulation autologue des follicules pileux pour arrêter la chute et régénérer la densité de la chevelure.",
                    Category = Category.Hair,
                    BeforeImageUrl = "/images/before_hair_3.png",
                    AfterImageUrl = "/images/after_hair_3.png",
                    ProtocolName = "PRP & REGENERA ACTIVA CAPILLAIRE",
                    Sessions = "4 séances",
                    ResultType = "Regénératif"
                }
            };

            foreach (var res in initialResults)
            {
                var exists = context.Results.Any(r => r.Title == res.Title);
                if (!exists)
                {
                    context.Results.Add(res);
                    changesSaved = true;
                }
            }

            if (changesSaved)
            {
                context.SaveChanges();
            }
        }
    }
}
