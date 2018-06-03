using osu.Framework.Audio;
using osu.Framework.Configuration;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using Symcol.Core.Graphics.Containers;
using touhou.sharp.Game.Config;

namespace touhou.sharp.Game.Graphics
{
    public class THSharpSkinElement : SymcolContainer
    {
        public override bool HandleMouseInput => false;
        public override bool HandleKeyboardInput => false;

        private string loadedSkin;

        private readonly Storage storage;
        private readonly THSharpConfigManager config;

        private ResourceStore<byte[]> skinResources;
        private TextureStore skinTextures;
        //TODO: implement audio here for "skinning" it
        //private AudioManager skinAudio;

        private readonly ResourceStore<byte[]> thSharpResources;
        private readonly TextureStore thSharpTextures;
        //TODO: Implement "GetSkinAudioElement" and "GetAudioElement"
        //private readonly AudioManager thSharpAudio;

        public THSharpSkinElement(Storage storage, THSharpConfigManager config)
        {
            this.storage = storage;
            this.config = config;

            if (thSharpResources == null)
            {
                thSharpResources = new ResourceStore<byte[]>();
                thSharpResources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore("touhou.sharp.Game.Resource.dll"), ("Assets")));
                thSharpResources.AddStore(new DllResourceStore("touhou.sharp.Game.Resource.dll"));
                thSharpTextures = new TextureStore(new RawTextureLoaderStore(new NamespacedResourceStore<byte[]>(thSharpResources, @"Textures")));
                thSharpTextures.AddStore(new RawTextureLoaderStore(new OnlineStore()));

                var tracks = new ResourceStore<byte[]>(thSharpResources);
                tracks.AddStore(new NamespacedResourceStore<byte[]>(thSharpResources, @"Tracks"));
                tracks.AddStore(new OnlineStore());

                var samples = new ResourceStore<byte[]>(thSharpResources);
                samples.AddStore(new NamespacedResourceStore<byte[]>(thSharpResources, @"Samples"));
                samples.AddStore(new OnlineStore());

                //thSharpAudio = new AudioManager(tracks, samples);
            }
        }

        /// <summary>
        /// Will attempt to get a skin element fron the skin, if no element is found return the default element
        /// </summary>
        /// <param name="stockTextures"></param>
        /// <param name="skin"></param>
        /// <param name="fileName"></param>
        /// <param name="storage"></param>
        /// <returns></returns>
        private Texture GetSkinTextureElement(string fileName)
        {
            Texture texture = null;

            string skin = config.GetBindable<string>(THSharpSetting.Skin);
            Storage skinStorage = storage.GetStorageForDirectory("Skins\\" + skin);

            if (skin == "Default")
            {
                texture = thSharpTextures.Get(fileName + ".png");
                return texture;
            }

            if (loadedSkin != skin)
            {
                loadedSkin = skin;
                skinResources = new ResourceStore<byte[]>(new StorageBackedResourceStore(skinStorage));
                skinTextures = new TextureStore(new RawTextureLoaderStore(skinResources));
                //TODO: load skin audio samples here
            }

            if (skinStorage.Exists(fileName + ".png"))
            {
                texture = skinTextures.Get(fileName + ".png");
                texture.ScaleAdjust = 1f;
            }

            return texture;
        }

        /// <summary>
        /// Will attempt to get a skin element from the skin, if no element is found return null
        /// </summary>
        /// <param name="skin"></param>
        /// <param name="fileName"></param>
        /// <param name="storage"></param>
        /// <returns></returns>
        private Texture GetTextureElement(string fileName)
        {
            Texture texture = null;

            string skin = config.GetBindable<string>(THSharpSetting.Skin);
            Storage skinStorage = storage.GetStorageForDirectory("Skins\\" + skin);

            if (loadedSkin != skin)
            {
                loadedSkin = skin;
                skinResources = new ResourceStore<byte[]>(new StorageBackedResourceStore(skinStorage));
                skinTextures = new TextureStore(new RawTextureLoaderStore(skinResources));
            }

            if (skinStorage.Exists(fileName + ".png"))
            {
                texture = skinTextures.Get(fileName + ".png");
                texture.ScaleAdjust = 1f;
            }

            return texture;
        }
    }
}
