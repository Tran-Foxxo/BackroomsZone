using GDWeave;
using GDWeave.Modding;

namespace GDWeave.Sample;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        modInterface.RegisterScriptMod(new StuckPatch());
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
