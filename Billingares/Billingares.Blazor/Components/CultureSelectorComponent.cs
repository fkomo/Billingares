using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace Billingares.Blazor.Components
{
	public partial class CultureSelectorComponent
	{
		[Inject]
		public NavigationManager NavManager { get; set; }

		[Inject]
		public IJSRuntime JSRuntime { get; set; }

		[Inject]
		protected ApplicationState AppState { get; set; }

		readonly CultureInfo[] cultures = new[]
		{
			new CultureInfo("en-US"),
			new CultureInfo("en-GB"),           
			new CultureInfo("sk-SK")
		};

		CultureInfo Culture
		{
			get => CultureInfo.CurrentCulture;
			set
			{
				if (CultureInfo.CurrentCulture != value)
				{
					var js = (IJSInProcessRuntime)JSRuntime;
					js.InvokeVoid("blazorCulture.set", value.Name);
					NavManager.NavigateTo($"{ NavManager.BaseUri }{ AppState.ClientId }", forceLoad: true);
				}
			}
		}

		public string GetFlagUrl(CultureInfo culture)
		{
			return culture.Name switch
			{
				"en-GB" => "https://upload.wikimedia.org/wikipedia/en/a/ae/Flag_of_the_United_Kingdom.svg",
				"de-DE" => "https://upload.wikimedia.org/wikipedia/en/b/ba/Flag_of_Germany.svg",
				"en-US" => "https://upload.wikimedia.org/wikipedia/en/a/a4/Flag_of_the_United_States.svg",
				"sk-SK" => "https://upload.wikimedia.org/wikipedia/commons/e/e6/Flag_of_Slovakia.svg",
				_ => null,
			};
		}
	}
}
