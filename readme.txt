Brook wires up views to view models both at design time (in Blend) and at runtime. Wiring up of views is achieved through a simple attached property. Brook uses a convention based approach to do the wireup with it's strong point being blendability.


1. Run Build.cmd
2. Open Expression Blend 4 and then BrookSample.sln from within Blend.
3. Open MainPage or MainPageView and you will see it's VM is injected into it with a design time service which displays "Design time"
4. Open BrookSample in Vs, launch the project and you will see the same view with it's vm injected with a runtime service which displays "Runtime"

Solutions:

Brook.sln - Builds the Brook framework. Also includes BlendLauncher for step-through debugging of design time executing code.
BrookSample.sln - contains a very simple sample (stupidly simply) using Brook to wire up VMs. Uses pure reflection (no attributes need) or MEF depending on if the UseMef flag is set.


Projects:

Book\Brook.csproj - contains the core library for wiring VMs
Brook.Mef\Brook.Mef.csproj - contains MEF extensions for injecting with Mef at runtime.
BrookSample\BrookSample.csproj - The sample
DesignTimeServices\DesignTimeServices.csporj - contains design time VM and service for BrookSample to be viewed in Blend
BlendLauncher\BlendLauncher.csproj - Dummy project for launching Blend. Useful for stepping through Brook code while it is executing at design time. Right click to set as startup project in Brook.sln.



