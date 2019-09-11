using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models;
using Server;
using SoapCore;

namespace soapErrors
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ISampleService, SampleService>();

            services.AddSoapCore();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
    

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();



            var transportBinding = new HttpTransportBindingElement();
            var textEncodingBinding = new TextMessageEncodingBindingElement(MessageVersion.Soap12WSAddressing10, System.Text.Encoding.UTF8);
            var customBinding = new CustomBinding(transportBinding, textEncodingBinding);
            app.UseSoapEndpoint<ISampleService>("/MyService12.asmx", customBinding, SoapSerializer.XmlSerializer);


//			app.UseSoapEndpoint<ISampleService>("/Service.svc", new BasicHttpBinding(), SoapSerializer.DataContractSerializer);
			app.UseSoapEndpoint<ISampleService>("/Service.asmx", new BasicHttpBinding(), SoapSerializer.XmlSerializer);


            var textBindingElement11 = new TextMessageEncodingBindingElement(MessageVersion.Soap11, System.Text.Encoding.UTF8);
            var textBindingElement12 = new TextMessageEncodingBindingElement(MessageVersion.Soap12WSAddressing10, System.Text.Encoding.UTF8);
            var httpBindingElement = new HttpTransportBindingElement
            {
                MaxBufferSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue
            };
            var customBindingCombined = new CustomBinding(textBindingElement11, textBindingElement12, httpBindingElement);
            app.UseSoapEndpoint<ISampleService>("/MyService1112.asmx", customBindingCombined, SoapSerializer.XmlSerializer);


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
