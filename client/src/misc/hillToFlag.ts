export default function (hill: string) {
  const map = [
    ['Finland K105', '🇫🇮'],
    ['Switzerland K170', '🇨🇭'],
    ['Czech Republic K135', '🇨🇿'],
    ['Belarus K220', '🇧🇾'],
    ['Austria K70', '🇦🇹'],
    ['USA K130', '🇺🇸'],
    ['Latvia K165', '🇱🇻'],
    ['Poland K80', '🇵🇱'],
    ['Japan K210', '🇯🇵'],
    ['Belgium K95', '🇧🇪'],
    ['Iceland K190', '🇮🇸'],
    ['England K50', '🇬🇧'],
    ['Germany K120', '🇩🇪'],
    ['Estonia K155', '🇪🇪'],
    ['Norway K90', '🇳🇴'],
    ['Australia K240', '🇦🇺'],
    ['Ireland K125', '🇮🇪'],
    ['Ukraine K60', '🇺🇦'],
    ['Hungary K180', '🇭🇺'],
    ['Sweden K140', '🇸🇪'],
    ['Italy K230', '🇮🇹'],
    ['Denmark K75', '🇩🇰'],
    ['Slovakia K110', '🇸🇰'],
    ['Canada K185', '🇨🇦'],
    ['Lithuania K145', '🇱🇹'],
    ['Kazakhstan K85', '🇰🇿'],
    ['China K205', '🇨🇳'],
    ['France K160', '🇫🇷'],
    ['Holland K100', '🇳🇱'],
    ['Russia K200', '🇷🇺'],
    ['Korea K150', '🇰🇷'],
    ['Slovenia K250', '🇸🇮'],
  ]

  return map.find(x => hill === x[0])![1]
}
