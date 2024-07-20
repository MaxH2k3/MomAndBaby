const paginationTable = (eachPage, nameTable) => {
    var items = document.getElementById(nameTable).querySelectorAll('.table-body tr');
    var currentPage = 1;

    showPage(currentPage, eachPage, items);
    currentPage = createPagination(currentPage, eachPage, items, nameTable);

}



function showPage(pageNumber, eachPage, items) {
    var startIndex = (pageNumber - 1) * eachPage;
    var endIndex = startIndex + eachPage;

    for (var i = 0; i < items.length; i++) {
        if (i >= startIndex && i < endIndex) {
            items[i].style.display = 'table-row';
        } else {
            items[i].style.display = 'none';
        }
    }
}

function createPagination(currentPage, eachPage, items, nameTable) {
    var pagination = document.getElementById(nameTable).getElementsByClassName('pagination')[0];

    var pageCount = Math.ceil(items.length / eachPage);
    var startPage = currentPage - 1;
    var endPage = currentPage + 1;

    if (startPage < 1) {
        startPage = 1;
        endPage = Math.min(3, pageCount);
    }

    if (endPage > pageCount) {
        endPage = pageCount;
        startPage = Math.max(1, pageCount - 2);
    }

    pagination.innerHTML = '';

    if (currentPage > 1) {
        var prevLi = document.createElement('li');
        var prevLink = document.createElement('a');
        prevLink.href = '#';
        prevLink.classList.add('page-link');
        prevLink.textContent = 'Prev';
        prevLi.classList.add('page-item');
        prevLi.appendChild(prevLink);
        pagination.appendChild(prevLi);

        prevLink.addEventListener('click', function (e) {
            e.preventDefault();
            currentPage--;
            showPage(currentPage, eachPage, items);
            createPagination(currentPage, eachPage, items, nameTable);
            return currentPage;
        });
    }

    for (var i = startPage; i <= endPage; i++) {
        var li = document.createElement('li');
        var a = document.createElement('a');
        a.href = '#';
        a.textContent = i;
        a.classList.add('page-link');
        li.appendChild(a);
        pagination.appendChild(li);

        a.addEventListener('click', function (e) {
            e.preventDefault();
            var pageNumber = parseInt(this.textContent);
            currentPage = pageNumber;
            showPage(pageNumber, eachPage, items);
            createPagination(currentPage, eachPage, items, nameTable);
            return currentPage;
        });

        if (i === currentPage) {
            li.classList.add('active');
        }

        li.classList.add('page-item');
    }

    if (currentPage < pageCount) {
        var nextLi = document.createElement('li');
        var nextLink = document.createElement('a');
        nextLink.href = '#';
        nextLink.classList.add('page-link');
        nextLink.textContent = 'Next';
        nextLi.classList.add('page-item');
        nextLi.appendChild(nextLink);
        pagination.appendChild(nextLi);

        nextLink.addEventListener('click', function (e) {
            e.preventDefault();
            currentPage++;
            showPage(currentPage, eachPage, items);
            createPagination(currentPage, eachPage, items, nameTable);
            return currentPage;
        });
    }
}


