
const bar = document.getElementById('bar');
const close = document.getElementById('close');
const nav = document.getElementById('navbar');

if(bar){
    bar.addEventListener('click',()=>{
        nav.classList.add('active');
    });
}

if(close){
    close.addEventListener('click',()=>{
        nav.classList.remove('active');
    });
}

var count = 0;
var decreaseButton = document.getElementById("decreaseButton");
var increaseButton = document.getElementById("increaseButton");
var countDisplay = document.getElementById("countDisplay");
var addToCartButton = document.getElementById("addToCartButton");

decreaseButton.addEventListener("click", function() {
  if (count > 0) {
    count--;
    countDisplay.innerHTML = count;
  }
});

increaseButton.addEventListener("click", function() {
  count++;
  countDisplay.innerHTML = count;
});

addToCartButton.addEventListener("click", function() {
  alert("Ürün sepete eklendi: " + count + " adet");
});


var scrollTopButton = document.getElementById("scrollTopButton");

window.addEventListener("scroll", function() {
  if (window.pageYOffset > 100) {
    scrollTopButton.classList.add("show");
  } else {
    scrollTopButton.classList.remove("show");
  }
});

scrollTopButton.addEventListener("click", function() {
  window.scrollTo({ top: 0, behavior: "smooth" });
});

