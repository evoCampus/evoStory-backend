﻿namespace EvoStory.BackendAPI.DTO
{
    public class CreateUserDTO
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
