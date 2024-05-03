using HardwareStore.ApplicationController.ThridParty.IoC.Abstractions;
using HardwareStore.ApplicationController.ThridParty.IoC.Abstractions.Common;

namespace HardwareStore.ApplicationController.ThridParty.IoC.Autofac;

public interface IAutofacContainerBuilder : IIoCContainerBuilder
{
    IIoCContainerBuilder RegisterGeneric<TService>(Lifetime lifetime);

    IIoCContainerBuilder RegisterWithConstructor<TService>(string nameParameter, object parameter, Lifetime lifetime = Lifetime.Transient, bool asSelf = false)
        where TService : notnull;

    IIoCContainerBuilder RegisterGeneric<TService, TImplementation>(Lifetime lifetime)
        where TImplementation : TService;
}