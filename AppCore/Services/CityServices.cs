using AppCore.IServices;
using Domain.Interfaces;
using System.Threading.Tasks;
using Domain.Entities;

namespace AppCore.Services
{
    public class CityServices : IServices<City.Root>
    {
        protected IModel<City.Root> model;
       

        public CityServices(IModel<City.Root> model)
        {
            this.model = model;
           
        }

        public Task<string> GetIcon()
        {
            return model.GetIcon();
        }

        public Task<string> GetIconLocal()
        {
            return model.GetIconLocal();
        }

        public Task<City.Root> GetLocal_Location()
        {
            return model.GetLocal_Location();
        }

        public Task<City.Root> GetWather(string city)
        {
            return model.GetWather(city);
        }

       
    }
}
