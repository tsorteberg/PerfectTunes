/***************************************************************
* Name        : PerfectTunesUnitOfWork.cs
* Author      : Tom Sorteberg
* Created     : 04/19/2021
* Course      : CIS 174
* Version     : 1.0
* OS          : Windows 10 Pro, Visual Studio Community 2019
* Copyright   : This is my own original work based on
*               specifications issued by our instructor
* Description : Final Project.
* I have not used unauthorized source code, either modified or 
* unmodified. I have not given other fellow student(s) access 
* to my program.         
***************************************************************/
namespace PerfectTunes.Models
{
    public class FinalUnitOfWork : IFinalUnitOfWork
    {
        private PerfectTunesContext context { get; set; }
        public FinalUnitOfWork(PerfectTunesContext ctx) => context = ctx;

        private Repository<Instrument> instrumentData;
        public Repository<Instrument> Instruments
        {
            get
            {
                if (instrumentData == null)
                    instrumentData = new Repository<Instrument>(context);
                return instrumentData;
            }
        }

        private Repository<Brand> brandData;
        public Repository<Brand> Brands
        {
            get
            {
                if (brandData == null)
                    brandData = new Repository<Brand>(context);
                return brandData;
            }
        }

        private Repository<Department> departmentData;
        public Repository<Department> Departments
        {
            get
            {
                if (departmentData == null)
                    departmentData = new Repository<Department>(context);
                return departmentData;
            }
        }

        public void Save() => context.SaveChanges();
    }
}