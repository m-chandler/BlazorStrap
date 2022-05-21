using Microsoft.AspNetCore.Components;
using BlazorStrap;

namespace BlazorStrap_Docs.Shared
{
    public partial class Header
    {
        [CascadingParameter]
        public MainLayout? Parent { get; set; }
        private string _sidebarButtonClass = "bd-sidebar-toggle d-md-none collapsed";


        private string? Selected { get; set; }

        private List<string> _themes = new List<string>();
        protected override void OnInitialized()
        {
            _themes = Enum.GetNames(typeof(Theme)).ToList();
            if (Parent != null)
                Parent.OnSideBarShown += OnSideBarShown;
        }

        protected override async Task OnAfterRenderAsync(bool firstrun)
        {
            if (firstrun)
            {
                await _blazorStrap.SetBootstrapCss("5.1.3");
            }
        }

        private async Task SelectedChanged(string value)
        {
            Selected = value;
            await _blazorStrap.SetBootstrapCss(value, "5.1.3");
        }
        private async Task OnSideBarShown(bool state)
        {
            if(state)
                _sidebarButtonClass = "bd-sidebar-toggle d-md-none";
            else
                _sidebarButtonClass = "bd-sidebar-toggle d-md-none collapsed";
            await InvokeAsync(StateHasChanged);
        }
        public void Dispose()
        {
            if (Parent != null)
                Parent.OnSideBarShown -= OnSideBarShown;
        }
    }
}