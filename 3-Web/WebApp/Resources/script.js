window.onload = (event) => {
    console.log("page is fully loaded");
};

function displaySection(sectionName) {
    document.getElementById('statut').style.display = 'none';
    document.getElementById('controls').style.display = 'none';
    document.getElementById(sectionName).style.display = '';
    //switch (sectionName) {
    //    case 'statut':
    //        document.getElementById('statut').style.display = '';
    //        document.getElementById('controls').style.display = 'none';
    //        break;
    //    case 'controls':
    //        document.getElementById('statut').style.display = 'none';
    //        document.getElementById('controls').style.display = '';
    //        break;
    //}
}