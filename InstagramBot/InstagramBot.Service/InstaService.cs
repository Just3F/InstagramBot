using System;
using System.Threading.Tasks;

namespace InstagramBot.Service
{
    public class InstaService
    {
        private bool _isEnabled;

        public void Run()
        {
            _isEnabled = true;
        }

        public void Stop()
        {
            _isEnabled = false;
        }

        public void Build()
        {
            while (_isEnabled)
            {
                
            }

            Console.WriteLine("Instagram service is stopped.");
        }
    }
}
