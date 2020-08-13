using Service;

namespace BLL
{
    public class BLLClass:IBLL
    {
        IService service;

        public BLLClass(IService _service)
        {
            this.service = _service;
        }

        public string GetBussines()
        {
            return service.GetService() + " king";
        }

    }
}
