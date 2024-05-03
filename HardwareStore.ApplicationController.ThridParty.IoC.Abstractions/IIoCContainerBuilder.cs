using HardwareStore.ApplicationController.ThridParty.IoC.Abstractions.Common;

namespace HardwareStore.ApplicationController.ThridParty.IoC.Abstractions;

public interface IIoCContainerBuilder
{
    IIoCContainerBuilder Register<TService>(Lifetime lifetime = Lifetime.Transient, bool asSelf = false)
        where TService : notnull;

    IIoCContainerBuilder RegisterInstance<TInstance>(TInstance instance, Lifetime lifetime = Lifetime.Transient, bool asSelf = false)
        where TInstance : class;

    IIoCContainerBuilder RegisterView<TView, TImplementation>(Lifetime lifetime = Lifetime.Transient, bool asSelf = false)
        where TView : notnull
        where TImplementation : TView;

    IIoCContainerBuilder Register<TService, TImplementation>(Lifetime lifetime = Lifetime.Transient, bool asSelf = false)
        where TImplementation : TService;

    TService Resolve<TService>()
        where TService : notnull;

    IIoCContainerBuilder RegisterGeneric<TService>(Lifetime lifetime);

    IIoCContainerBuilder RegisterWithConstructor<TService>(string nameParameter, object parameter, Lifetime lifetime = Lifetime.Transient, bool asSelf = false)
        where TService : notnull;

    IIoCContainerBuilder RegisterGeneric<TService, TImplementation>(Lifetime lifetime)
        where TImplementation : TService;

    void Build();
}