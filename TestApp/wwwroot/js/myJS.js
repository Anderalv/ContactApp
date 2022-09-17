
$(document).ready(function() {
    $(document).on('click',".btnFormEdit",function () {

        let block = $(this).parent().parent();

        let name = block.find('.contact-name').text();
        let phone = block.find('.contact-phone').text().slice(7);
        let jobTitle = block.find('.contact-jobTitle').text().slice(11);
        let birthDate = block.find('.contact-birthDate').text().slice(12);
        let id = block.find('#contact-id').val();

        $('#id-edit').val(id);
        $('#name-edit').val(name);
        $('#phone-edit').val(phone);
        $('#jobTitle-edit').val(jobTitle);
        $('#birthDate-edit').val(birthDate);

        $("#myModalEdit").modal('show');
    });
});

$(document).ready(function(){
    $("#open-modal").click(function(){
        $("#myModal").modal('show');
    });
});


const checkPhone = function(Phone){
    return Phone.charAt(0) !== "+" || Phone.length !== 13;
}

const checkName = function(Name){
    return Name.length < 2 || Name.charAt(0) !== Name.charAt(0).toUpperCase();
}

const checkBirthDate = function (BirthDate){
    return BirthDate.charAt(2) !== "/" || BirthDate.charAt(5) !== "/" || BirthDate.length !== 10
}

const generateError = function (text) {
    const error = document.createElement('div')
    error.className = 'error'
    error.style.color = 'red'
    error.innerHTML = text
    return error
}

const removeValidation = function (Form) {

    const form = document.getElementById(Form)

    const errors = form.querySelectorAll('.error')

    for (var i = 0; i < errors.length; i++) {
        errors[i].remove()
    }
}

const validate = function (action) {

    let Name;
    let Phone;
    let JobTitle;
    let BirthDate;
    let Form;
    let StyleBorder = "2px solid red";

    if (action === "addContact"){
        Form = "myForm"
        Name = document.getElementById("name");
        Phone = document.getElementById("phone");
        JobTitle = document.getElementById("jobTitle");
        BirthDate = document.getElementById("birthDate");
    }else{
        Form = "myForm-edit";
        Name = document.getElementById("name-edit");
        Phone = document.getElementById("phone-edit");
        JobTitle = document.getElementById("jobTitle-edit");
        BirthDate = document.getElementById("birthDate-edit");
    }


    removeValidation(Form);
    Name.style.border = null;
    Phone.style.border = null;
    JobTitle.style.border = null;
    BirthDate.style.border = null;


    let error;
    if(checkName(Name.value)) {
        error = generateError("Wrong format(example: Vasiliy)")
        Name.parentElement.insertBefore(error, Name)
        Name.style.border = StyleBorder;
        return false;
    }

    if (checkPhone(Phone.value)) {
        error = generateError("Wrong format(example: +375291234567)")
        Phone.parentElement.insertBefore(error, Phone)
        Phone.style.border = StyleBorder;
        return false;
    }

    if(!JobTitle.value) {
        error = generateError("Can't be empty")
        JobTitle.parentElement.insertBefore(error, JobTitle)
        JobTitle.style.border = StyleBorder;
        return false;
    }

    if(checkBirthDate(BirthDate.value)) {
        error = generateError("Wrong format(example: 01/01/2001)")
        BirthDate.parentElement.insertBefore(error, BirthDate)
        BirthDate.style.border = StyleBorder;
        return false;
    }
    
    return true;
}