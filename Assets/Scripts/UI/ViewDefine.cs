using System.Collections.Generic;
using Scripts.UI;

namespace Scripts.UI
{
    public enum ViewID
    {
        None = 0,
        Example = 1,
    }

    public enum OverlayID
    {
        none = 0,
        Example = 1,
    }

    public class ViewData<TViewControllerType> : IViewData
        where TViewControllerType : class, IViewController, new()
    {
        public ViewID _viewID;
        public OverlayID _overlayID = OverlayID.none;

        public ViewData(ViewID nViewID)
        {
            _viewID = nViewID;
        }

        public ViewData(ViewID nViewID, OverlayID nOverlayID)
        {
            _viewID = nViewID;
            _overlayID = nOverlayID;
        }

        public IViewController GenerateViewController()
        {
            return new TViewControllerType();
        }

        public string GetViewPrefabPath()
        {
            return ResourcePath.GetPanelPath(_viewID.ToString());
        }

        public string GetOverlayPath()
        {
            if(_overlayID == OverlayID.none)
                return string.Empty;
            return ResourcePath.GetOverlayPath(_overlayID.ToString());
        }
    }

    public interface IViewData
    {
        public IViewController GenerateViewController();
        public string GetViewPrefabPath();
        public string GetOverlayPath();
    }

    public class ViewDefine
    {
        public static readonly Dictionary<ViewID, IViewData> VIEW_DIC = new Dictionary<ViewID, IViewData>()
        {
            {ViewID.Example, new ViewData<UIExampleController>(ViewID.Example, OverlayID.Example)},
        };

        public static bool TryGetViewData(ViewID nViewId, out IViewData nData)
        {
            if (VIEW_DIC.TryGetValue(nViewId, out nData))
            {
                return true;
            }

            return false;
        }
    }
}