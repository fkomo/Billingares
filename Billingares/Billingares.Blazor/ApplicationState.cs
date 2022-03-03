using Billingares.Blazor.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ujeby.Blazor.Base;

namespace Billingares.Blazor
{
    public interface IBillingaresApplicationState : IApplicationState
	{
        void UpdateClaims(Claim[] claims, Claim[] ignoredClaims, bool optimizeTransactions);

        bool FancyMode { get; set; }

        Claim[] Claims { get; }

        string ClientId { get; set;  }

        bool OptimizeTransactions { get; }
    }

    public class ApplicationState : ApplicationStateBase, IBillingaresApplicationState
    {
        [Inject]
        private IStringLocalizer<Resource> localizer { get; set; }

        public string ClientId { get; set; }

        public ApplicationState()
        {
            ClientId = Guid.NewGuid().ToString("N");
        }

        public ApplicationState(IStringLocalizer<Resource> _localizer) : this()
        {
            localizer = localizer ?? _localizer;
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
            set { _darkMode = value; }
        }

        public string HomeUrl => $"/{ ClientId }";

        public string ApplicationTitle => localizer["ApplicationTitle"];

        public void UpdateClaims(Claim[] claims, Claim[] ignoredClaims, bool optimizeTransactions)
        {
            Claims = claims.Except(ignoredClaims).ToArray();

            OptimizeTransactions = optimizeTransactions;

            NotifyStateChanged();
        }
	}
}
