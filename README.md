2026-persiapan_PDBL-backend

Deskripsi
Proyek ini adalah layanan backend untuk Sistem Manajemen Peminjaman Ruangan. Dibuat untuk memusatkan data reservasi ruangan, melacak status peminjaman secara real-time, dan menyediakan API yang andal untuk proyek PDBL PENS 2026.

Fitur Utama
- CRUD Peminjaman Ruangan: Membuat, membaca, memperbarui dan menghapus data reservasi ruangan.
- Manajemen Status: Mengubah status peminjaman antara Pending, Disetujui, dan Ditolak.

- Soft Deletes: Mekanisme keamanan untuk melindungi data agar tidak terhapus secara permanen secara tidak sengaja.

- Pencarian & Filter: Melacak dan memfilter riwayat peminjaman untuk kebutuhan evaluasi.

Tech Stack
- Framework: ASP.NET Core.
- Database: SQL Server / MySQL.
- Version Control: Git & GitHub.
- Project Management: GitHub Project Board.

Instalasi
Clone repository:
git clone https://github.com/[USERNAME_KAMU]/2026-persiapan_PDBL-backend.git

Masuk ke direktori proyek:
cd 2026-persiapan_PDBL-backend

Restore dependencies:
dotnet restore

Penggunaan
Jalankan migrasi database:
dotnet ef database update
Jalankan aplikasi : 
dotnet run

Akses Dokumentasi API (Swagger) di: https://localhost:[5xxx]/swagger

Variabel Lingkungan (Environment Variables)
Pengaturan yang diperlukan di file .env atau appsettings.json:
- ConnectionStrings:DefaultConnection: String koneksi database Anda.
- ASPNETCORE_ENVIRONMENT: Setel ke Development atau Production.

Kontribusi
Proyek ini adalah bagian dari tugas pendahuluan PDBL 2026. Kontribusi saat ini dibatasi untuk evaluator proyek dan anggota tim.

Lisensi
Proyek ini dilisensikan di bawah MIT License.

Kredit / Info Penulis
Dikembangkan oleh Diaz Raharjo 
STr Teknik Informatika - Politeknik Elektronika Negeri Surabaya.