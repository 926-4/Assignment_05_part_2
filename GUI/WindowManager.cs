namespace UBB_SE_2024_Team_42.GUI
{
    public class WindowManager // I strongly believe this should not exist - team lead of Team42
    {
        private Repository.Repository repository;
        private Service.Service service;

        public Repository.Repository Repository // what the hell
        {
            get => repository;
            set { repository = value; }
        }

        public Service.Service Service
        {
            get => service;
            set { service = value; }
        }
        public WindowManager()
        {
            repository = new Repository.Repository();
            service = new Service.Service(repository);
        }
    }
}
