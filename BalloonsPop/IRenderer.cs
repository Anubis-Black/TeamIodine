namespace BalloonsPop
{
    using System.Collections.Generic;

    public interface IRenderer
    {
        void RenderObjects(IList<IRenderable> renderableObjects);
    }
}
