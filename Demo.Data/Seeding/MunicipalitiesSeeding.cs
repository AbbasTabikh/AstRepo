using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Seeding
{
    public static class MunicipalitiesSeeding
    {
        public static void SeedMunicipalities(this ModelBuilder builder)
        {
            builder.Entity<Municipality>().HasData(
            
            new Municipality
            {
                Id = 1,
                ArabicName = "بعبدا",
                EnglishName = "Baabda",
            },
            
            new Municipality
            {
                Id = 2,
                ArabicName = "عبودية",
                EnglishName = "Abboudieh"
            },



            new Municipality
            {
                Id = 3,
                ArabicName = "بعلبك",
                EnglishName = "Baalbeck"
            },

            new Municipality
            {
                Id = 4,
                ArabicName = "بريتال",
                EnglishName = "Brital"
            },


            new Municipality
            {
                Id = 5,
                ArabicName = "دورس",
                EnglishName = "Douris"
            },



            new Municipality
            {
                Id = 6,
                ArabicName = "شارون",
                EnglishName = "Charoun"
            },

            new Municipality
            {
                Id = 7,
                ArabicName = "شرتون",
                EnglishName = "Chartoun"
            },


            new Municipality
            {
                Id = 8,
                ArabicName = "كفرمتى",
                EnglishName = "Kfarmatta"
            },



            new Municipality
            {
                Id = 9,
                ArabicName = "صوفر",
                EnglishName = "Saoufar"
            },


            new Municipality
            {
                Id = 10,
                ArabicName = "عبادية",
                EnglishName = "Abadiyeh"
            },

             new Municipality
             {
                 Id = 11,
                 ArabicName = "زفتا",
                 EnglishName = "Zefta"
             }

            );



        }
    }
}
