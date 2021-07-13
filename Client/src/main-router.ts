import { LogManager, autoinject, PLATFORM } from "aurelia-framework";
import { RouterConfiguration, Router } from 'aurelia-router';

export var log = LogManager.getLogger('app.main-router');

@autoinject
export class MainRouter {
  router: Router;

  configureRouter(config: RouterConfiguration, router: Router) {
    this.router = router;

    config.options.pushState = true;
    config.options.root = '/';

    config.map([
      {
        route: '', name: 'home', title: "PostOffice Demo",
        moduleId: PLATFORM.moduleName('pages/home')
      },

      {
        route: 'shipment/index', name: 'shipmentIndex', title: "Shipments",
        moduleId: PLATFORM.moduleName('pages/shipment/index')
      }, {
        route: 'bag/index', name: 'bagIndex', title: "Bags",
        moduleId: PLATFORM.moduleName('pages/bag/index')
      }, {
        route: 'parcel/index', name: 'parcelIndex', title: "Parcels",
        moduleId: PLATFORM.moduleName('pages/parcel/index')
      },
    ]);
  }
}
