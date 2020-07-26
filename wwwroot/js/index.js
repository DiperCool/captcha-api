let url="https://localhost:5001/"
let sessionKey="";
let code="";


let button= document.querySelector("#get").addEventListener("click",async ()=>{
    let code= document.querySelector("#code").value;
    let response= await fetch(`${url}create/apikey`,{
        method:"POST",
        headers:{
            "CodeCaptcha":code,
            "SessionCaptcha":sessionKey
        }
    });
    if(response.status>=400){
        alert("invalid");
        return;
    }
    let apikey=await response.text();
    document.querySelector("#apikey").value=apikey;


})

let genCaptcha=async()=>{
    let response= await fetch(`${url}generateCaptcha`,{
        method:"POST"
    });
    let img= document.querySelector("#captcha");
    let res=await response.json();
    sessionKey=res.sessionKey;
    img.setAttribute("src", `data:image/png;base64, ${res.image}`);
    
}
genCaptcha();

