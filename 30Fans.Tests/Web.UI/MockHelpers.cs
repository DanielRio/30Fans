using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Moq;

namespace _30Fans.Tests.Web.UI {
    public class MockHelpers {
        public static HttpContextBase GetFakeContext() {
            var context = new Mock<HttpContextBase>();
            //we need these for our context 
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();


            context.Setup(ct => ct.Request).Returns(request.Object);
            context.Setup(ct => ct.Response).Returns(response.Object);
            context.Setup(ct => ct.Session).Returns(session.Object);
            context.Setup(ct => ct.Server).Returns(server.Object);

            return context.Object;
        }

        public static HttpRequest GetFakeRequest() {
            var request = new Mock<HttpRequest>();
            return request.Object;
        }

        public static HttpResponse GetFakeResponse() {
            var response = new Mock<HttpResponse>();
            return response.Object;
        }
    }// class
}
