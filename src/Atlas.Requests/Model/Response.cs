using Atlas.Core.Models;
using Atlas.Requests.Interfaces;

namespace Atlas.Requests.Model
{
    public class AuthResponse<T> : IAuthResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Result { get; set; }
        public Authorisation? Authorisation { get; set; }
    }
}
