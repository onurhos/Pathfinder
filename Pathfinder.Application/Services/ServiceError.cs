using System;

namespace Pathfinder.Application.Services
{
    public class ServiceError
    {
        public string[] Messages { get; }
        
        private ServiceError() { }
        public ServiceError(string[] messages) : this()
        {
            Messages = messages;
        }

        public void PrintToConsole()
        {
            foreach (var message in Messages)
            {
                Console.WriteLine(message);
            }
        }
    }
}