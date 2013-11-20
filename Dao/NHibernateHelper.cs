using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using Dao.Mapping;
using System.Reflection;

namespace Dao {
    public class NHibernateHelper {
        private static ISessionFactory _sessionFactory;
        private static Configuration _configuration;
        private static HbmMapping _hbmMapping;

        public static ISession OpenSession() {
            return SessionFactory.OpenSession();
        }

        public static Configuration Configuration {
            get {
                if (_configuration == null) {
                    //Create the nhibernate configuration
                    _configuration = CreateConfiguration();
                }
                return _configuration;
            }
        }

        public static ISessionFactory SessionFactory {
            get {
                if (_sessionFactory == null) {
                    _sessionFactory = Configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static HbmMapping Mapping {
            get {
                if (_hbmMapping == null) {
                    _hbmMapping = CreateMapping();
                }
                return _hbmMapping;
            }
        }

        private static Configuration CreateConfiguration() {
            var configuration = new Configuration();

            configuration.Configure();

            //Adding Connection String (Removing duplicate code hibernate.cfg and web.config)
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            configuration.DataBaseIntegration(x => {
                x.ConnectionString = connectionString;
            });

            configuration.AddDeserializedMapping(Mapping,null);

            return configuration;
        }

        private static HbmMapping CreateMapping(){
            var mapper = new ModelMapper();
            Type[] types = { 
                typeof(ProductMap), 
                typeof (CategoryMap),
                typeof (CategoryItemMap),
                typeof (PhotoMap)
            };

            mapper.AddMappings(types);           

            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            return mapping;
        }
    }// class
}
