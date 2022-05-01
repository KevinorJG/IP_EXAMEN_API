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

        public Task<City.Root> GetWather()
        {
            return model.GetWather();
        }

        public string Recibir(string city)
        {
           return model.Recibir(city);  
        }
    }
}
