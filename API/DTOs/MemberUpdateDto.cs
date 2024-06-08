using System;

namespace API.DTOs
{
    public class MemberUpdateDto
    {
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        // Add more properties as needed

        // Add any additional methods or constructors here
    }
}