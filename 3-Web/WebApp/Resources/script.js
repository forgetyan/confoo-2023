window.onload = (event) => {
    console.log("page is fully loaded");
    getControllerStatus();
    getLedStatus();
    setTimeout(getTemperatureData, 500)
};

function displaySection(sectionName) {
    document.getElementById('statut').style.display = 'none';
    document.getElementById('controls').style.display = 'none';
    document.getElementById(sectionName).style.display = '';
}

function getControllerStatus() {
    const request = new Request('/api/status', { method: 'GET' });
    fetch(request)
        .then((response) => {
            if (response.status === 200) {
                return response.json();
            } else {
                throw new Error('Something went wrong on API server!');
            }
        })
        .then((response) => {
            document.getElementById("controllerIp").innerText = response.Ip;
            console.debug(response);
            // …
        }).catch((error) => {
            console.error(error);
        });

}

function getLedStatus() {
    const request = new Request('/api/led', { method: 'GET' });
    fetch(request)
        .then((response) => {
            if (response.status === 200) {
                return response.json();
            } else {
                throw new Error('Something went wrong on API server!');
            }
        })
        .then((response) => {
            document.getElementById("ledSwitch").checked = response.IsActive;
            document.getElementById("ledSpeed").value = response.Speed;
            console.debug(response);
            // …
        }).catch((error) => {
            console.error(error);
        });
}

var _lastTemperature = 21;
var _tempOn = true;
function getTemperatureData() {
    const request = new Request('/api/temperature', { method: 'GET' });
    if (_tempOn) {
        fetch(request)
            .then((response) => {
                if (response.status === 200) {
                    return response.json();
                } else {
                    throw new Error('Something went wrong on API server!');
                }
            })
            .then((response) => {
                document.getElementById("temperature").innerText = response.TemperatureCelcius.toFixed(2);
                _tempOn = response.IsActive;
                document.getElementById("tempSwitch").checked = _tempOn;
                if (_tempOn && _lastTemperature > response.TemperatureCelcius) {
                    tempArrow.className = "arrow down";
                } else if (_tempOn && _lastTemperature < response.TemperatureCelcius) {
                    tempArrow.className = "arrow up";
                } else {
                    tempArrow.className = "arrow right";
                }
                _lastTemperature = response.TemperatureCelcius;
                if (response.AlarmHigh) {
                    document.getElementById("alert").style.display = "block";
                    document.getElementById("alert").className = "alert high";
                    document.getElementById("alertMessage").innerText = "La température est trop haute";
                } else if (response.AlarmLow) {
                    document.getElementById("alert").style.display = "block";
                    document.getElementById("alert").className = "alert low";
                    document.getElementById("alertMessage").innerText = "La température est trop basse";
                }
                else {
                    document.getElementById("alert").style.display = "none";
                }
                console.debug(response);
                setTimeout(getTemperatureData, 1000);
            }).catch((error) => {
                console.error(error);
            });
    } else {
        setTimeout(getTemperatureData, 1000);
    }
}

function setLedActive(active) {
    const request = new Request('/api/led/active', { method: 'POST', body: active ? '1' : '0' });
    fetch(request)
        .then((response) => {
            if (response.status === 200) {
                return response;
            } else {
                throw new Error('Something went wrong on API server!');
            }
        })
        .then((response) => {
            console.debug(response);
            // …
        }).catch((error) => {
            console.error(error);
        });
}

function setLedSpeed(speed) {
    const request = new Request('/api/led/speed', { method: 'POST', body: speed });
    fetch(request)
        .then((response) => {
            if (response.status === 200) {
                return response;
            } else {
                throw new Error('Something went wrong on API server!');
            }
        })
        .then((response) => {
            console.debug(response);
            // …
        }).catch((error) => {
            console.error(error);
        });
}

function setTempActive(active) {
    const request = new Request('/api/temperature/active', { method: 'POST', body: active ? '1' : '0' });
    fetch(request)
        .then((response) => {
            if (response.status === 200) {
                return response;
            } else {
                throw new Error('Something went wrong on API server!');
            }
        })
        .then((response) => {
            _tempOn = active;
            console.debug(response);
            // …
        }).catch((error) => {
            console.error(error);
        });
}
