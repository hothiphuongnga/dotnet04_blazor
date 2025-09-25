window.fetchdata = async () => {
    const response = await fetch("https://svcy.myclass.vn/api/ProductApi/getall");
    const data = await response.json();
    // document.getElementById("data_api").innerText = JSON.stringify(data);
    return data;
}