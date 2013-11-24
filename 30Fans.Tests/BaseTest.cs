using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using NHibernate.Tool.hbm2ddl;
using Dao;
using _30Fans.Tests.Builder;

namespace _30Fans.Tests {    
    [TestClass]
    public class BaseTest {
        //Factories to remove the noise of always call new builders...
        protected CategoryBuilder Category() {
            return new CategoryBuilder();
        }

        protected CategoryItemBuilder CategoryItem() {
            return new CategoryItemBuilder();
        }

        protected ProductBuilder Product() {
            return new ProductBuilder();
        }        
    }// class
}
