import numeral from 'numeral';
import moment from 'moment';

/* PRICE */

export class CurrencyFormatValueConverter {
  toView(value) {
    if (value == null) {
      return "? €";
    }
    return numeral(value).format('0,0.00 $');
  }
}

/* OTHER */

export class DatetimeFormatValueConverter {
  toView(value) {
    return moment(value).format("DD.MM.YYYY HH:mm");
  }
}

export class DateFormatValueConverter {
  toView(value) {
    return moment(value).format("DD.MM.YYYY");
  }
}

export class PrintOrNoneValueConverter {
  toView(value) {
    return value ? value : "-";
  }
}

// A ValueConverter for iterating an Object's properties inside of a repeat.for in Aurelia
// (c) https://ilikekillnerds.com/2015/08/iterating-objects-using-repeat-for-in-aurelia/
export class ObjectKeysValueConverter {
  toView(obj) {
    // Create a temporary array to populate with object keys
    let temp = [];

    // A basic for..in loop to get object properties
    // https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Statements/for...in
    for (let prop in obj) {
      if (obj.hasOwnProperty(prop)) {
        temp.push(prop);
      }
    }

    return temp;
  }
}
