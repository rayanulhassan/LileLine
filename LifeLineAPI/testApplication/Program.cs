using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace testApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            using (lifelinedbEntities entities = new lifelinedbEntities())
            {

                Member val = new Member();
                val.age = 21;
                val.firstName = "hasa";
                val.lastName = "hasa";
                val.email = "hasa";
                val.password = "hasa";
                val.gender = "has";

                entities.Members.Add(val);
                entities.SaveChanges();

            }
        }
    }
}
