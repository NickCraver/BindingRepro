# Binding Redirects go boom.

This repo demonstrates a lack of binding reidrect generation when needed for runtime, even under .NET 4.7.2. This is for issue [dotnet/corefx #32511](https://github.com/dotnet/corefx/issues/32511).

In here we have:
- BindingRepro (sample console)
- RefPipelines (sample library)

What should be generated from the `<AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>` in BindingRepro.csproj is:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <dependentAssembly>
        <assemblyIdentity name="System.Buffers" publicKeyToken="cc7b13ffcd2ddd51" culture="neutral" />
        <bindingRedirect oldVersion="0.0.0.0-4.0.2.0" newVersion="4.0.3.0"/>
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
</configuration>
```

The relevant reference tree here is:

- BindingRepro
  - RefPipelines
    - System.Buffers 4.5.0 (4.0.3 assembly version)
    - System.IO.Pipelines 
      - System.Buffers 4.4.0 (4.0.2 assembly version)

...so when Pipelines touches System.Buffers (4.0.2) a binding redirect must be in place. This is not generated as it should be. So at runtime of `BindingRepro\bin\Debug\net472\BindingRepro.exe`, you'll get:
```
λ BindingRepro.exe
Going boom...

Unhandled Exception: System.IO.FileLoadException: Could not load file or assembly 'System.Buffers, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT: 0x80131040)
   at System.Buffers.ArrayMemoryPool`1.ArrayMemoryPoolBuffer..ctor(Int32 size)
   at System.Buffers.ArrayMemoryPool`1.Rent(Int32 minimumBufferSize)
   at RefPipelines.Boom.CantTouchThis() in C:\git\NickCraver\BindingRepro\RefPipelines\Boom.cs:line 8
   at BindingRepro.Program.Main(String[] args) in C:\git\NickCraver\BindingRepro\BindingRepro\Program.cs:line 11
```
Repro instuctions:
1. `dotnet build`
2. `BindingRepro\bin\Debug\net472\BindingRepro.exe`
