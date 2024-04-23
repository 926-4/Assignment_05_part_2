using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Team_42.Repository;
using UBB_SE_2024_Team_42.Service;
namespace UBB_SE_2024_Team_42.GUI
{
    public class WindowManager //I strongly believe this should not exist - team lead of Team42
    {
        private Repository.Repository repository; 
        private Service.Service service;

        public Repository.Repository Repository //what the hell
        {
            get { return repository; }
            set { repository = value; }
        }

        public Service.Service Service
        {
            get { return service; }
            set { service = value; }
        }
        public WindowManager()
        {
            repository = new Repository.Repository("String");
            service = new Service.Service(repository);
        }
    }
}
