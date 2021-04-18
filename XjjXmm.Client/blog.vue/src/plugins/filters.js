export const blogDateFormat = function (date) {
    if (!date) return ''

    //console.log(date);

    let index = date.indexOf("T");
    //const year = date.getFullYear();
    //const month = date.getMonth() + 1;
    //const day = date.getDate();

    
    return date.substring(0, index);
  };