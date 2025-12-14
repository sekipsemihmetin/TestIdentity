namespace TestIdentity.Aplication.DTOs.UserDTOs
{
    /// <summary>
    /// Kullanıcı DTO'su / User DTO
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Kullanıcı ID'si / User ID
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcı adı / Username
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// E-posta adresi / Email address
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// E-posta onaylı mı / Is email confirmed
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Telefon numarası / Phone number
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Telefon onaylı mı / Is phone confirmed
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// İki faktörlü kimlik doğrulama etkin mi / Is two factor authentication enabled
        /// </summary>
        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Ad / First name
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Soyad / Last name
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Aktif mi / Is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Roller / Roles
        /// </summary>
        public List<string> Roles { get; set; } = new();
    }
}

