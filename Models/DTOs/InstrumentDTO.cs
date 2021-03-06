/***************************************************************
* Name        : InstrumentDTO.cs
* Author      : Tom Sorteberg
* Created     : 04/21/2021
* Course      : CIS 174
* Version     : 1.0
* OS          : Windows 10 Pro, Visual Studio Community 2019
* Copyright   : This is my own original work based on
*               specifications issued by our instructor
* Description : Final Project
* I have not used unauthorized source code, either modified or
* unmodified. I have not given other fellow student(s) access
* to my program.
***************************************************************/
using System.Collections.Generic;

namespace PerfectTunes.Models
{
    public class InstrumentDTO
    {
        public int InstrumentId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Brand Brand { get; set; }

        public string LogoImage { get; set; }

        public void Load(Instrument instrument)
        {
            InstrumentId = instrument.InstrumentId;
            Name = instrument.Name;
            Price = instrument.Price;
            Brand = instrument.Brand;
            LogoImage = instrument.LogoImage;
        }
    }

}

