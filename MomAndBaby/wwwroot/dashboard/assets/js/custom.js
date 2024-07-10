/*Active menu*/
const changeActiveMenu = (currentMenu) => {
    const element = $(".tab-menu").eq(currentMenu);
    element.addClass('active');

    const parentElement = element.parent().parent();
    if(parentElement.hasClass('tab-menu-parent')) {
        parentElement.addClass('active open');
    }
}