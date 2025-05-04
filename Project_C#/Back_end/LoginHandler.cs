using System;

namespace Project_C_.Back_end
{
    public static class LoginHandler
    {
        // Automata: Login adalah salah satu state yang akan berpindah ke MainMenu jika sukses
        public static User Login(UserManager userManager, out State nextState, out User currentUser)
        {
            Console.WriteLine("Login (tulis '1' untuk kembali)");
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            // **Precondition**: Izinkan pengguna membatalkan dan kembali ke menu awal
            if (username == "1")
            {
                nextState = State.Home;  // Automata: Kembali ke Home jika user membatalkan
                currentUser = null;
                return null;
            }

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            // **Precondition**: Izinkan membatalkan juga di input password
            if (password.ToLower() == "back")
            {
                nextState = State.Home;  // Automata: Kembali ke Home jika user membatalkan
                currentUser = null;
                return null;
            }

            currentUser = null;
            nextState = State.Login;

            // **Precondition**: Pastikan username dan password tidak kosong
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Username and password cannot be empty.");
                return null;
            }

            try
            {
                currentUser = userManager.Authenticate(username, password);
                Console.WriteLine("Login successful!");
                nextState = State.MainMenu;  // Automata: Berpindah ke MainMenu setelah login sukses
                return currentUser;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                nextState = State.Login;  // Automata: Tetap di Login jika gagal
                return null;
            }
        }
    }
}
