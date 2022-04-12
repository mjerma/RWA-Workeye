function ListBoxValid(sender, args) {
    var lbProjekti = document.getElementById(sender.controltovalidate);
    args.IsValid = lbProjekti.options.length > 0;
}