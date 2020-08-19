using LibraryManagementSystem.Classes.BAL;
using LibraryManagementSystem.Classes.DAL;
using LibraryManagementSystem.Interfaces.BAL;
using LibraryManagementSystem.Interfaces.DAL;
using LibraryManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace LibraryManagementSystem.Classes
{
    public class DependencyInjector
    {
        private static readonly UnityContainer unityContainer = new UnityContainer();

        public static void Configure()
        {
            unityContainer.RegisterType<IBookDataManager, BookDataManager>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IBookManager, BookManager>(new ContainerControlledLifetimeManager());

            unityContainer.RegisterType<ICustomerDataManager, CustomerDataManager>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<ICustomerManager, CustomerManager>(new ContainerControlledLifetimeManager());

            unityContainer.RegisterType<IValidation, Validation>(new ContainerControlledLifetimeManager());

            unityContainer.RegisterType<BookSearchVM>(new ContainerControlledLifetimeManager());
        }

        public static T Retrieve<T>()
        {
            return unityContainer.Resolve<T>();
        }
    }
}