using AutoMapper;
using DocumentsStorage.Infra.Data.Context;
using DocumentsStorage.Infra.Data.Interfaces;
using DocumentsStorage.Infra.Data.Repository;
using DocumentsStorage.Service.Interfaces;
using DocumentsStorage.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocumentsStorage.Infra.CrossCutting.IoC
{
    public static class SimpleInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddAutoMapper(GetAutoMapperProfilesFromAllAssemblies().ToArray());

            services.AddScoped<IDocumentService, DocumentService>();

            // Infra - Data
            services.AddScoped<IDocumentRepository, DocumentRepository>();

            services.AddScoped<DocumentContext>();
        }

        /// <summary>
        /// See article below
        /// https://stackoverflow.com/questions/40275195/how-to-set-up-automapper-in-asp-net-core
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Type> GetAutoMapperProfilesFromAllAssemblies()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var aType in assembly.GetTypes())
                {
                    if (aType.IsClass && !aType.IsAbstract && aType.IsSubclassOf(typeof(Profile)))
                    {
                        yield return aType;
                    }
                }
            }
        }
    }
}
