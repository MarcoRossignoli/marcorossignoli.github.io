using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Keys.Data.Enums;

namespace Keys.Data.Entities
{
    public class Key
    {
        [Required]
        public int Id { [ExcludeFromCodeCoverage]get; set; }

        [Required]
        public Guid Uuid { get; set; }

        [Required]
        public Guid Kid { get; set; }

        [Required]
        public KeyType KeyType { get; set; }

        [Required]
        public string ContentKey { get; set; }
    }
}
