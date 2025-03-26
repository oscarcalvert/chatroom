using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using chatroom.Models;

namespace chatroom.Controllers
{
    public class AuthController : BaseController
    {
        private DBContext _db = new DBContext();

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            Console.WriteLine("Active Users: ");
            var userList = _db.Users.ToList();
            foreach (var u in userList)
            {
                Console.WriteLine();
                Console.WriteLine("Info:");
                Console.WriteLine(u.Username);
                Console.WriteLine(u.PasswordHash);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(User model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "invalid login";
                return View(model);
            }

            if (!_db.Users.Any(u => u.Username == model.Username))
            {
                ViewBag.Error = "invalid login";
                return View(model);
            }

            var user = _db.Users.FirstOrDefault(u => u.Username == model.Username);
            if (user?.PasswordHash != model.PasswordHash)
            {
                ViewBag.Error = "invalid login";
                return View(model);
            }
            var uid = user.UserId;
            HttpContext.Session.SetInt32("UID", uid);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_db.Users.Any(u => u.Username == model.Username))
            {
                ModelState.AddModelError("Username", "Username is taken");
                return View(model);
            }

            _db.Users.Add(model);
            _db.SaveChanges();
            HttpContext.Session.SetInt32("UID", model.UserId);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete()
        {
            var user = _db.Users.Find(ViewBag.User.UserId);
            if (user == null)
            {
                Console.WriteLine("User not to delete found");
            }

            _db.Users.Remove(user);
            _db.SaveChanges();
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public static string HashPassword(string password)
        {
            // Generate a salt
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            // Create hash
            byte[] hash;
            using (var pbkdf2 = new Rfc2898DeriveBytes(
                       password,
                       salt,
                       iterations: 10000,
                       HashAlgorithmName.SHA256))
            {
                hash = pbkdf2.GetBytes(20);
            }

            // Combine salt and hash
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Convert to base64 string
            return Convert.ToBase64String(hashBytes);
        }


        public static bool VerifyPassword(string password, string savedPasswordHash)
        {
            // Convert base64 hash back to byte array
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

            // Extract the salt
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Compute hash of input password
            byte[] hash;
            using (var pbkdf2 = new Rfc2898DeriveBytes(
                       password,
                       salt,
                       iterations: 10000,
                       HashAlgorithmName.SHA256))
            {
                hash = pbkdf2.GetBytes(20);
            }

            // Compare hash bytes
            return CryptographicOperations.FixedTimeEquals(
                hash,
                new ReadOnlySpan<byte>(hashBytes, 16, 20)
            );
        }
    }
}