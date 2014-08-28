using Dao.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace _30Fans.Web
{
    public class TeamController : ApiController
    {
        private ProductDao _productDao;

        public TeamController()
        {
            _productDao = new ProductDao();
        }

        public class TeamModel
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Country { get; set; }

            public string FlagUrl { get; set; }

            public List<string> PhotosUrl { get; set; }

            public string PayPalCode { get; set; }

            public int PhotosCount
            {
                get;
                set; 
            }


        }

        // GET api/<controller>
        public IEnumerable<TeamModel> GetAllTeams()
        {
            var products = _productDao.GetaAll().Select(p => new TeamModel{
                Id = int.Parse(p.Id.ToString()),
                Name = p.ProductName,
                Country = p.CategoryItem.ItemName
            });

            return products;
        }

        // GET api/<controller>/5
        public TeamModel GetTeam(int id)
        {
            List<string> fanImages = new List<string>();
            try
            {
                fanImages = System.IO.Directory.GetFiles(System.Web.Hosting.HostingEnvironment.MapPath("~/Images/Fans/2014/" + id)).ToList();
                for (int i = 0; i < fanImages.Count; i++)
                {
                    fanImages[i] = Path.GetFileName(fanImages[i]);
                }
            }
            catch
            {
            }
            
            var product = _productDao.Get(id);

            TeamModel team = new TeamModel()
            {
                Id = id,
                Country = product.CategoryItem.ItemName,
                Name = product.ProductName,
                FlagUrl = product.ImageUrl,
                PayPalCode = product.PaymentCode,
                PhotosUrl = fanImages,
                PhotosCount = fanImages.Count
            };

            return team;
        }


        /*
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
         */
    }
}