﻿using System;

namespace Project_C_.Back_end
{
    public static class RegisterHandler
    {
        // Automata: Register adalah state yang berpindah ke Login setelah sukses
        public static void Register(UserManager userManager, out State nextState)
        {
            Console.WriteLine("Register an account (tulis '1' untuk kembali)");

            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            // **Precondition**: Izinkan membatalkan dan kembali ke menu awal
            if (username == "1")
            {
                nextState = State.Home;  // Automata: Kembali ke Home jika user membatalkan
                return;
            }

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            // **Precondition**: Izinkan membatalkan juga di input password
            if (password == "1")
            {
                nextState = State.Home;  // Automata: Kembali ke Home jika user membatalkan
                return;
            }

            Console.Write("Enter nama: ");
            string nama = Console.ReadLine();

            // **Precondition**: Izinkan membatalkan juga di input password
            if (nama == "1")
            {
                nextState = State.Home;  // Automata: Kembali ke Home jika user membatalkan
                return;
            }

            Console.Write("Enter NIK: ");
            string nik = Console.ReadLine();
            // **Precondition**: Izinkan membatalkan juga di input password
            if (nik == "1")
            {
                nextState = State.Home;  // Automata: Kembali ke Home jika user membatalkan
                return;
            }

            Console.Write("Enter RT: ");
            string rt = Console.ReadLine();
            // **Precondition**: Izinkan membatalkan juga di input password
            if (rt == "1")
            {
                nextState = State.Home;  // Automata: Kembali ke Home jika user membatalkan
                return;
            }

            Console.Write("Enter RW: ");
            string rw = Console.ReadLine();
            // **Precondition**: Izinkan membatalkan juga di input password
            if (rw == "1")
            {
                nextState = State.Home;  // Automata: Kembali ke Home jika user membatalkan
                return;
            }

            nextState = State.Register;

            Role role = Role.Masyarakat;  // Role default: Masyarakat

            try
            {
                userManager.Register(username, password, role, nama, nik, rt, rw);
                Console.WriteLine("Registration successful! You can log in now.");
                nextState = State.Login;  // Automata: Berpindah ke Login setelah register sukses
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                nextState = State.Register;  // Automata: Tetap di Register jika terjadi kesalahan
            }
        }
    }
}
