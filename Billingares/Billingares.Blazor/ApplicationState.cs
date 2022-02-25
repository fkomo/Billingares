using Ujeby.Blazor.Base;

namespace Billingares.Blazor
{
    public class ApplicationState : ApplicationStateBase
    {
        public string ClientId { get; set; }

        public ApplicationState()
		{
            ClientId = Guid.NewGuid().ToString("N");
        }

        public Claim[] Claims { get; private set; } = Array.Empty<Claim>();
        public bool OptimizeTransactions { get; private set; }

        private bool _fancyMode = false;
        public bool FancyMode
        { 
            get { return _fancyMode; }
            set 
            { 
                _fancyMode = value;              
                NotifyStateChanged();
            }  
        }

        private bool _darkMode = true;
        public bool DarkMode
        { 
            get { return _darkMode; }
            set {  _darkMode = value; }  
        }

        public void UpdateClaims(Claim[] claims, Claim[] ignoredClaims, bool optimizeTransactions)
        {
            Claims = claims.Except(ignoredClaims).ToArray();

            OptimizeTransactions = optimizeTransactions;

            NotifyStateChanged();
        }
	}
}
