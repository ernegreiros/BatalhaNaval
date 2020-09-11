import React from 'react';

const DataFormatter = {
  FormatChampionshipResponse: (championships) => {
    return championships.map((championship) => {
      let temp = { ...championship };

      if (temp.isPrivate)
        temp.lock = <i style={{ marginLeft: "100%" }} className="fa fa-lock center tooltipped" data-position="top" aria-hidden="true" data-tooltip="Campeonato privado. Precisa de chave para inscrição."></i>;
      else
        temp.lock = "";

      if (temp.isRegistrationPeriod)
        temp.isRegistrationPeriod = <h6 className="textGreen"><b>Abertas</b></h6>;
      else
        temp.isRegistrationPeriod = <h6 className="textRed"><b>Encerradas</b></h6>;

      temp.gameMode = DataFormatter.GetGameModeFormatted(temp.gameMode);
      temp.startDateUTC = DataFormatter.FormatDate(temp.startDateUTC);
      temp.endDateUTC = DataFormatter.FormatDate(temp.endDateUTC);
      temp.arrow = <i style={{ marginRight: "10%" }} className="fa fa-chevron-circle-right fa-lg" aria-hidden="true"></i>;
      return temp;
    });
  },
  AddIconToRank: (teams) =>{
    return teams.map((team) =>{
      let temp = {...team};

      temp.arrow =  <i style={{ marginRight: "10%" }} className="fa fa-chevron-circle-right fa-lg" aria-hidden="true"></i>

      return temp;
    })
  },
  FormatDate: (date) => {
    var dateObj = new Date(date);
    var day = DataFormatter.GetTwoDigitDay(dateObj);
    var month = DataFormatter.GetTwoDigitMonth(dateObj);
    var hour = DataFormatter.GetTwoDigitHour(dateObj);
    var minutes = DataFormatter.GetTwoDigitMinutes(dateObj);
    var year = dateObj.getFullYear();
    return `${day}/${month}/${year} ${hour}:${minutes}h`
  },
  FormatDateToUTCString: (date) => {
    var dateObj = new Date(date);
    return dateObj.toISOString();
  },
  FormatDateOnlyDayAndMonth: (date) => {
    var dateObj = new Date(date);
    var day = DataFormatter.GetTwoDigitDay(dateObj);
    var month = DataFormatter.GetTwoDigitMonth(dateObj);
    return `${day}/${month}`;
  },
  FormatDateOnlyTime: (date) => {
    var dateObj = new Date(date);
    var hour = DataFormatter.GetTwoDigitHour(dateObj);
    var minutes = DataFormatter.GetTwoDigitMinutes(dateObj);
    return `${hour}:${minutes}`;
  },
  FormatDateTimeLocal: () => {
    var dateObj = new Date(Date.now());
    var day = DataFormatter.GetTwoDigitDay(dateObj);
    var month = DataFormatter.GetTwoDigitMonth(dateObj);
    var year = dateObj.getFullYear();

    return `${year}-${month}-${day}T00:00`;
  },
  GetTwoDigitHour: (date) => {
    var hour = date.getHours();
    return hour = ("0" + hour).slice(-2);
  },
  GetTwoDigitMinutes: (date) => {
    var minutes = date.getMinutes();
    return minutes = ("0" + minutes).slice(-2);
  },
  GetTwoDigitMonth: (date) => {
    var month = date.getMonth() + 1;
    return month = ("0" + month).slice(-2);
  },
  GetTwoDigitDay: (date) => {
    var day = date.getDate();
    return day = ("0" + day).slice(-2);
  },
  FormatPlayersNames: (players) => {
    return players.map((player) => {
      let temp = { ...player };
      let idInit = temp.activisionId.search("#");
      var onlyName = temp.activisionId.slice(0, idInit);
      temp.activisionId = onlyName;
      return temp;
    })
  },
  TrimField: (value) => {
    return value.trim();
  },
  GetGameModeFormatted: (gameMode) => {
    switch (gameMode) {
      case 'br_brsolo': return "BR Solo";
      case 'br_brduos': return "BR Duos";
      case 'br_brtrios': return "BR Trios";
      case 'br_brquads': return "BR Quads";
      default: return gameMode;
    }
  }
}

export default DataFormatter;