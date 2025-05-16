using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;

namespace LaptopInventory
{
    public class LoginService
    {
        private List<User> users;
        private readonly string filePath = "users.json";
        public User LoggedInUser { get; private set; }
        public LoginState CurrentLoginState { get; private set; } = LoginState.LoggedOut;

        private List<Item> items = new List<Item>
        {
            new Item { Id = "B001", Name = "Samsung Galaxy S21", Category = "HP", Quantity = 10 },
            new Item { Id = "B002", Name = "iPhone 13", Category = "HP", Quantity = 7 },
            new Item { Id = "B003", Name = "Dell XPS 13", Category = "Laptop", Quantity = 5 },
            new Item { Id = "B004", Name = "MacBook Pro", Category = "Laptop", Quantity = 3 }
        };

        public LoginService()
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            else
            {
                users = new List<User>();
            }
        }

        private void SaveUsers()
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        // ✅ Design by Contract (precondition)
        public bool Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username tidak boleh kosong.");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Password tidak boleh kosong.");

            LoggedInUser = users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (LoggedInUser != null)
            {
                CurrentLoginState = LoginState.LoggedIn; // automata: transition
                return true;
            }

            return false;
        }

        public void Logout()
        {
            LoggedInUser = null;
            CurrentLoginState = LoginState.LoggedOut; // automata: transition
        }

        // ✅ Parameterized Generic Add User + DbC
        public void AddUser<T>(T user) where T : User
        {
            if (users.Any(u => u.Username == user.Username))
                throw new InvalidOperationException("Username sudah digunakan.");

            users.Add(user);
            SaveUsers();
        }

        // ✅ Table-driven logic
        public string HandleByRole(string role)
        {
            var roleActions = new Dictionary<string, string>
            {
                { "admin", "Dashboard Admin" },
                { "user", "Menu Utama Pengguna" }
            };

            return roleActions.ContainsKey(role) ? roleActions[role] : "Peran tidak dikenali";
        }

        public List<User> GetSortedUsersByUsername()
        {
            return users.OrderBy(u => u.Username).ToList();
        }

        public List<Item> GetItems(string category = null, bool sortByName = false)
        {
            IEnumerable<Item> query = items;

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(i => i.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            if (sortByName)
            {
                query = query.OrderBy(i => i.Name);
            }

            return query.ToList();
        }
    }
}

