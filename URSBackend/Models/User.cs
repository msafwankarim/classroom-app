using Newtonsoft.Json;

namespace URSBackend.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }

        public User( string name,  int id)
        {
            
            Name = name;
            Id = id;
           
        }
        public User() { }

        public virtual string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}