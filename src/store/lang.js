import { reactive } from 'vue'

export const langState = reactive({
  current: 'TH',
  
  // Dictionary helper
  t: (th, en) => {
    return langState.current === 'TH' ? th : en;
  }
})

export const setLang = (lang) => {
  langState.current = lang
}
